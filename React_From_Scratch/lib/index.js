function createElement(type, props, ...children) {
    return{
        type,
        props: {
            ...props,
            children: children.map(child =>
                typeof child === "object"
                ? child
                : creatTextElement(child)
            ),
        },
    }
}

function creatTextElement(text) {
    return{
        type: "TEXT_ELEMENT",
        props: {
            nodeValue: text,
            children: [],
        },
    }
}

function createDom(fiber) {
    const dom = fiber.type == "TEXT_ELEMENT" ? document.createTextNode("") : document.createElement(fiber.type)
    updateDom(dom, {}, fiber.props)
    return dom
}

const isEvent = key => key.startsWith("on")
const isProperty = key => key != "children" && !isEvent(key)
const isNew = (prev, next) => key => prev[key] != next[key]
const isGone = (prev, next) => key => !(key in next)
function updateDom(dom, prevProps, nextProps) {
    Object.keys(prevProps).filter(isEvent).filter(
        key => !(key in nextProps) || isNew(prevProps, nextProps)(key)
    ).forEach(name => {
        const eventType = name.toLowerCase().substring(2)
        dom.removeEventListener(eventType, prevProps[name])
    })

    Object.keys(prevProps).filter(isProperty).filter(isGone(prevProps, nextProps)).forEach(name => {
        dom[name] = ""
    })

    Object.keys(nextProps).filter(isProperty).filter(isNew(prevProps, nextProps)).forEach(name => {
        dom[name] = nextProps[name]
    })

    Object.keys(nextProps).filter(isEvent).filter(isNew(prevProps, nextProps)).forEach(name => {
        const eventType = name.toLowerCase().substring(2)
        dom.addEventListener(eventType, nextProps[name])
    })
}

function commitRoot(){
    deletions.forEach(commitWork)
    commitWork(wipRoot.child)
    currentRoot = wipRoot
    wipRoot = null
}

function commitWork(fiber) {
    if(!fiber) return

    const domParent = fiber.parent.dom
    if(fiber.effectTag === "PLACEMENT" && fiber.dom != null) {
        domParent.appendChild(fiber.dom)
    }
    else if(fiber.effectTag === "UPDATE" && fiber.dom != null) {
        updateDom(fiber.dom, fiber.alternative.props, fiber.props)
    }
    else if(fiber.effectTag === "DELETION") {
        domParent.removeChild(fiber.dom)
    }
    commitWork(fiber.child)
    commitWork(fiber.sibling)
}

function render(element, container) {
    wipRoot = {
        dom: container,
        props: {
            children: [element],
        },
        alternative: currentRoot,
    }
    deletions = []
    nextUnitOfWork = wipRoot
}

let nextUnitOfWork = null
let currentRoot = null
let wipRoot = null
let deletions = null

function workLoop(deadline) {
    let shouldYeild = false
    while(nextUnitOfWork && !shouldYeild) {
        nextUnitOfWork = perfromUnitOfWork(nextUnitOfWork)
        shouldYeild = deadline.timeRemaining() < 1
    }
    if(!nextUnitOfWork && wipRoot) {
        commitRoot()
    }
    requestIdleCallback(workLoop)
}

requestIdleCallback(workLoop)

function perfromUnitOfWork(fiber) {
    if(!fiber.dom) {
        fiber.dom = createDom(fiber)
    }
    
    const elements = fiber.props.children
    reconcileChildren(fiber, elements)

    if(fiber.child) return fiber.child
    let nextFiber = fiber
    while(nextFiber) {
        if(nextFiber.sibling) return nextFiber.sibling
        nextFiber = nextFiber.parent
    }
}

function reconcileChildren(wipFiber, elements) {
    let index = 0
    let oldFiber = wipFiber.alternative && wipFiber.alternative.child
    let prevSibling = null

    while(index < elements.length || oldFiber != null) {
        const element = elements[index]
        let newFiber = null

        const sameType = oldFiber && element && element.type == oldFiber.type
        if(sameType) {
            newFiber = {
                type: oldFiber.type,
                props: element.props,
                dom: oldFiber.dom,
                parent: wipFiber,
                alternative: oldFiber,
                effectTag: "UPDATE",
            }
        }
        if(element && !sameType) {
            newFiber = {
                type: element.type,
                props: element.props,
                dom: null,
                parent: wipFiber,
                alternative: null,
                effectTag: "PLACEMENT",
            }
        }
        if(oldFiber && !sameType) {
            oldFiber.effectTag = "DELETION"
            deletions.push(oldFiber)
        }

        if(oldFiber) {
            oldFiber = oldFiber.sibling
        }

        if(index === 0) {wipFiber.child = newFiber}
        prevSibling.sibling = newFiber
        prevSibling = newFiber
        index++
    }

}

const Diadect = {
    createElement,
    render,
}


/** @jsx Didact.createElement */
const element = (
    <div id="foo">
      <a>bar</a>
      <b />
    </div>
  )

const container = document.getElementById("root")
Diadect.render(element, container)
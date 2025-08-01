const element = {
    type: "h1",
    props: {
        title: "New",
        children: "Title",
    },
}

const container = document.getElementById("root")
const node = document.createElement(element.type)
node["title"] = element.props.title

const text = document.createTextNode("")
text["nodeValue"] = element.props.children

node.appendChild(text)
container.appendChild(node)
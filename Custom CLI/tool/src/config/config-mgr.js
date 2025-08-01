const logger = require('../logger')('config:mgr');
const {cosmiconfigSync} = require('cosmiconfig');
const schema = require('./schema.json');
const betterAjvErrors = require('better-ajv-errors').default;
const Ajv = require('ajv').default;
const ajv = new Ajv();
const configLoader = cosmiconfigSync('tool');

module.exports = function getConfig() {
    const result = configLoader.search(process.cwd());
    if(!result) {
        logger.warning('Could not find configuration, using default');
        return {port: 1234};
    }
    else {
        const isValid = ajv.validate(schema, result.config);
        if(!isValid) {
            logger.warning(chalk.yellow('Invalid configuration found'));
            console.log();
            console.log(betterAjvErrors(schema, result.config, ajv.errors));
            process.exit(1);
        }
        logger.warning('Found configuration', JSON.stringify(result.config));
        return result.config;
    }
}
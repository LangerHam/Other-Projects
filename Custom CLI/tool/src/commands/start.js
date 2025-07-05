const logger = require('../logger')('commands:start');

module.exports = function start(config) {
    logger.highlight('Starting the application...');
    logger.debug('Received configuration in start -', config);
}
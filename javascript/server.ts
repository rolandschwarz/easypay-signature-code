var express = require('express');
var bodyParser = require('body-parser');
var errorHandler = require('errorhandler');
var router = require('./router');
var http = require('http');

// Setup server
var app = express();

app.use(bodyParser.json());
app.use(router);
app.use(errorHandler()); // Error handler - has to be last

app.set('views', __dirname + '/views');
app.set('view engine', 'jade');

var server = http.createServer(app);

// Start server
server.listen('3000', 'localhost', () => {
    console.log('Express server listening on %d, in %s mode', 3000, 'test');
});

var express = require('express');
var bodyParser = require('body-parser');
var errorHandler = require('errorhandler');
var router = require('./router');
var http = require('http');
var app = express();
app.use(bodyParser.json());
app.use(router);
app.use(errorHandler());
app.set('views', __dirname + '/views');
app.set('view engine', 'jade');
var server = http.createServer(app);
server.listen('3000', 'localhost', function () {
    console.log('Express server listening on %d, in %s mode', 3000, 'test');
});

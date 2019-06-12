var express = require('express');
var server = express();
var options = {
index: 'index.html'
};
server.use('/', express.static('/home/site/wwwroot', options));
app.get('*', function(req, res) {
    res.sendfile('./index.html')
  })
server.listen(process.env.PORT);
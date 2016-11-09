var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(3002);

io.on('connection', function(socket){
	socket.on("move",function(dataG){
		console.log(dataG);
	});
});
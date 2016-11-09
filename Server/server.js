var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(3002);

io.on('connection', function(socket){
	console.log(socket.id);
	
	socket.on("newRoom",function(dataG){
		
		console.log(dataG);
		socket.broadcast.emit("roomCreated", dataG);
	});
	
	socket.on("joinRoom",function(dataG){
		
		console.log(dataG);
		socket.broadcast.emit("joinedRoom", dataG);
	});
	
	socket.on("move",function(dataG){
		console.log(dataG);
	});
	
	socket.on("dead",function(dataG){
		
		
	});
});
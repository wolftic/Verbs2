var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(3002);

var rooms = [];

io.on('connection', function(socket){
	console.log(socket.id);
	
	socket.emit("name",{
		name: socket.id,
	});
	
	socket.on("newRoom",function(dataG){
		console.log("New room created: ");
		console.log(dataG);
		rooms.push(dataG);
		socket.broadcast.emit("roomCreated", dataG);
	});
	
	socket.on("joinRoom",function(dataG){
		for(var i = 0; i < rooms.length; i++ ){
			if(rooms[i].id == dataG.id) {
				rooms[i].players.push(dataG.players[0]);
				//socket.broadcast.emit("joinedRoom", rooms[i]);
				io.sockets.emit("joinedRoom", rooms[i]);
				console.log(rooms[i]);
			}
		}
	});
	
	socket.on("move",function(dataG){
		socket.broadcast.emit("OnMove",dataG);
	});
	
	socket.on("StartGame",function(){
		socket.broadcast.emit("otherStart");
	});
	
	socket.on("dead",function(dataG){
		socket.broadcast.emit("OnDead", dataG);
	});
	
	socket.on("respawn",function(dataG){
		socket.broadcast.emit("OnRespawn", dataG);
	});
});
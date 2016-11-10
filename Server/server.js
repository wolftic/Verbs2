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
	
	if(rooms.length != 0) {
		socket.emit("roomCreated", rooms[0]);
	}
	
	socket.on("onOtherStart",function(dataG){
		console.log("New player joined");
		socket.broadcast.emit("otherStart", dataG);
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
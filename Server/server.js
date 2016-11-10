var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(3003);

var activePlayers = [];

io.on('connection', function(socket){
	console.log(socket.id);
	
	var dataS = {
		name: socket.id,
	}
	
	activePlayers.push(dataS);
	
	socket.emit("name",dataS);
	
	for(var i = 0; i < activePlayers.length; i++){
		if(activePlayers[i].name != socket.id){
			console.log(activePlayers[i]);
			socket.emit("otherStart",activePlayers[i]);
		}
	}
	
	socket.on("onOtherStart",function(dataG){
		//console.log("New player joined");
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
	
	socket.on("disconnect", function(){
		for(var i = 0; i < activePlayers.length; i++){
			if(activePlayers[i].name == socket.id){
				console.log(socket.id + " dc");
				activePlayers.splice(i,1);
				socket.broadcast.emit("disconnection",activePlayers[i]);
			}
		}
	});
});
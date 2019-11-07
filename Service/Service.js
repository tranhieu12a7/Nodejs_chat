
var express=require("express");
var app=express();
var http=require("http").createServer(app);
var io = require("socket.io")(http);
var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/";


io.on('connection', function(socket){
    console.log('a user connected');
    socket.on("ChatMessenger",function(data){

     

        //check string nhan duoc co phai la chuoi json khong
        if(isJSON(data)==true)
        {
            console.log(data);
            var dataObj=JSON.parse(data);
            MongoClient.connect(url, function(err, db) {
                        if (err) throw err;
                        var dbo = db.db("DBLocalhost");
                        dbo.collection("DBMessenger").insertOne(dataObj, function(err, res)
                         {
                            if (err) throw err;
                            console.log("1 document inserted");
                            db.close();
                        });
                        var mysort = { createDate: -1 };
                        dbo.collection("DBMessenger").find().limit(1).sort(mysort).toArray(function(err,resuilt){
                            if (err) throw err;
                            var dataMess=JSON.stringify(resuilt);
                             console.log(dataMess);
                            io.emit("ChatMessenger",dataMess);
                            db.close();
                        });
                           
            });
        }
        else
        {
            if(data=="GetAll")
            {
             MongoClient.connect(url, function(err, db) {
                 if (err) throw err;
                 var dbo = db.db("DBLocalhost");
                 var mysort = { name: -1 };
                 dbo.collection("DBMessenger").find().sort(mysort).toArray(function(err,resuilt){
                     if (err) throw err;
                     var dataMess=JSON.stringify(resuilt);
                      console.log(dataMess);
                     io.emit("ChatMessenger",dataMess);
                     db.close();
                 });
             });
            }
            else
                 io.emit("ChatMessenger",data);
        }
    });
  });
  
  function isJSON(str){
    try{
        JSON.parse(str);
    }catch(err){
        return false;
    }
    return true;
  };

var server=http.listen(5000,function(){
    console.log("Nodejs service running...");
});
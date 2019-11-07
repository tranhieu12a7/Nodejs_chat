var mongodb= require("mongodb");
var Promise = require("bluebird");

var ConnectDB= function ConnectDB(){
    mongodb.Promise=Promise;
    const DB_CONNECTION="mongodb";
    const DB_HOST="localhost";
    const DB_PORT=27017;
    // const DB_NAME="Nodejs_Chats";
    const DB_NAME="DBLocalhost";
    const DB_USERNAME="";
    const DB_PASSWORD="";

    const URI=`${DB_CONNECTION}://${DB_HOST}:${DB_PORT}/${DB_NAME}`;

    return mongodb.connect(URI,{useMongoClient:true});
};

module.exports=ConnectDB;
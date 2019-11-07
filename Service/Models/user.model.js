var db= require("mongodb");
var Schema= db.Schema;
var UserShema=new Schema({
    Name: String,
    Messenger:{type: String, default:null},
    isInputChat:{type: Boolean,default:false},
    avartar : {type: String , default:"profile.png"}
});
module.exports= mongoose.model("Messenger",UserShema);
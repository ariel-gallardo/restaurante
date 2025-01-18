export default class ResponseServices{
    constructor() {
        this.statusCode = 0;
        this.message = '';
        this.messages = [];
        this.data = {};
        this.show = false;
        this.timeoutTime = process.env.RESPONSE_TIMEOUT;
        this.newData = this.newData.bind(this);
    }

    newData(newData){
        this.data = newData.content;
        let sI = newData.message.indexOf(`"`);
        this.message = newData.message.substring(0,sI);
        this.messages = newData.message.replace(`"`,'').split('|');
        if(this.messages)
            this.messages[0] = this.messages[0].replace(this.message,'');
        this.statusCode = newData.statusCode;
        this.show = true;
    }
  
}
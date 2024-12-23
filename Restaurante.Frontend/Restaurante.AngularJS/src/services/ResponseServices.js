export default class ResponseServices{
    constructor() {
        this.statusCode = 0;
        this.message = '';
        this.messages = [];
        this.data = {};
        this.show = false;
        this.timeoutTime = 3000;
        this.newData = this.newData.bind(this);
    }

    newData(newData){
        this.data = newData.content;
        if(newData.message.includes(`"`)){
            let temp = newData.message.split(' ');
            this.message = temp[0];
            this.messages = temp[1].split('|').map(x => x.replace(`"`,''));
        }
        else
        {
            if(newData.message.includes('|'))
            {
                this.messages = this.messages.split('|');
                this.message = '';
            }
            else
            {
                this.message = newData.message;
                this.messages = [];
            }
        }

        this.statusCode = newData.statusCode;
        this.show = true;
    }
  
}
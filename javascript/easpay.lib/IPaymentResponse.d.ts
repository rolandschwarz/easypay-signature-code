import MessageNS = require("./IMessage");

/**
* The response message list object represents the server messages, which can be extracted and interpreted  by the client.
**/
export interface IPaymentResponse {
    messages: MessageNS.IMessage[];
    success: boolean;
    error: any;
}

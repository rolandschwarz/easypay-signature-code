/**
* The response message object represents a server message, which can be extracted and interpreted by the client.
**/
export interface IMessage {
    /**
    * Localized message describing the nature of the problem reported
    **/
    message: string;

    /**
    * Name of the field from the request data model that this message is associated with
    **/
    field: string;

    /**
    * Symbolic error code identifying the type of error reported by this message
    * @seealso {Error}
    **/
    code: string;

    /**
    * The request id, sent within the XRequest-Id http header.
    **/
    requestId: string;

    success: boolean;
    error: string;
}

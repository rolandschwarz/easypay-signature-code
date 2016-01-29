export interface IStatusResponse {
    orderID: string;
    paymentInfo: string;
    isSilentAuthenticated: boolean;
    amount: number;
    msisdn: string;
    status: string;
    formattedMsisdn: string;
}

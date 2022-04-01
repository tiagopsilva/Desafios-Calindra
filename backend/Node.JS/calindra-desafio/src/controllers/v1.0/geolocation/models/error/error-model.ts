export class ErrorModel {

    constructor(statusCode: number, message: string | null, error: string, details: any = null) {
        this.statuCode = statusCode;
        this.message = message;
        this.error = error;
        this.details = details;
    }

    public statuCode: number;
    public message: string | null;
    public error: string;
    public details: any;
}
export enum HttpStatusCode {
    OK = 200,
    BadRequest = 400,
    NotFound = 404,
    InternalServerError = 500
}

export class ApiResponse {
    public statusCode: number;
    public body: string;

    constructor(statusCode: number, body: object) {
        this.statusCode = statusCode;
        this.body = JSON.stringify(body);
    }
}
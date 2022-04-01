export class Failure {
    private readonly _messages: string[] = []

    constructor(propertyName?: string) { 
        this.propertyName = propertyName?.trim() ?? '';
    }

    public propertyName: string;

    public get messages(): string[] {
        return new Array<string>(...this._messages);
    }

    public add(...messages: string[]) {
        const newMessages = messages.filter(message => !this._messages.includes(message));
        if (newMessages.length == 0)
            newMessages.push('Invalid value');
        newMessages.forEach(message => this._messages.push(message));
    }
}
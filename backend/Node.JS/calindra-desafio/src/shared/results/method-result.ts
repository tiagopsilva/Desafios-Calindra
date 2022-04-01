import { Failure } from './failure';

export class MethodResult {
    private readonly _failures: Failure[] = [];

    constructor() { }

    public data: object;
    public erroCode: number | null = null;

    public get success(): boolean {
        return this._failures.length == 0;
    }
    public get failure(): boolean {
        return this._failures.length > 0;
    }
    public get failures(): Failure[] {
        return new Array<Failure>(...this._failures);
    }
    public get count(): number {
        return this.failures.length;
    }

    public add(propertyName: string, ...messages: string[]): MethodResult {
        propertyName = propertyName?.trim() ?? '';
        let failure = this._failures.find(f => f.propertyName.toUpperCase() === propertyName.toUpperCase());
        if (failure) {
            failure.add(...messages);
        } else {
            failure = new Failure(propertyName);
        }
        return this;
    }
}
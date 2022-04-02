import { MethodResult } from './method-result';

export class Result {
    
    public static ok(data: object | null = null) {
        return Result.default(data);
    } 

    public static fail(propertyName: string, ...messages: string[]) {
        const methodResult = new MethodResult();
        methodResult.add(propertyName, ...messages);
        return methodResult;
    }

    public static failWithData(data: object, propertyName: string, ...messages: string[]) {
        const methodResult = Result.default(data);
        methodResult.add(propertyName, ...messages);
        return methodResult;
    }

    private static default(data: object | null = null) {
        const methodResult = new MethodResult();
        if (data != null)
            methodResult.data = data;
        return methodResult;
    }
}
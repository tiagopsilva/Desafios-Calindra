import { Response } from 'express';

import { MethodResult } from 'src/shared/results/method-result';
import { ErrorModel } from '../v1.0/geolocation/models/error/error-model';

export abstract class ControllerBase {

    protected process(result: MethodResult, response: Response): void {
        if (result.success) {
            response.json(result.data).status(200);
            return;
        }

        if (result.erroCode != null) {
            response
                .json(new ErrorModel(result.erroCode, 'Requisição inválida', 'Bad Request', result))
                .status(400);
            return;
        }

        response
            .json(new ErrorModel(500, null, 'Internal Server Error'))
            .status(500);
    }
}
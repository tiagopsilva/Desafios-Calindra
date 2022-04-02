import { ApiResponse, HttpStatusCode } from "../../shared/reponses/api-reponse.js";
import { MethodResult } from "../../shared/results/method-result.js";
import { Addresses } from "./models/addresses.js";
import { ErrorModel } from "./models/error/error-model.js";
import { DistanceCalculatorService } from "./services/distance-calculator.service.js";
import { GoogleGeolocationApiService } from "./services/google-geolocation-api.service.js";

module.exports.post = async (request: any) => {
    const googleGeolocationApiService = new GoogleGeolocationApiService();
    const distanceCalculatorService = new DistanceCalculatorService(googleGeolocationApiService);
    const addresses = (typeof request.body == "string" ? JSON.parse(request.body) : request.body) as Addresses;
    const result = await distanceCalculatorService.calculateDistancesFromAddressess(addresses);
    return process(result);
};

function process(result: MethodResult) {
    if (result.success) {
        return new ApiResponse(HttpStatusCode.OK, result.data);
    }
    if (result.erroCode != null && result.erroCode != HttpStatusCode.InternalServerError) {
        return new ApiResponse(HttpStatusCode.BadRequest, new ErrorModel(HttpStatusCode.BadRequest, 'Requisição inválida', 'Bad Request', result));
    }
    return new ApiResponse(HttpStatusCode.InternalServerError, new ErrorModel(HttpStatusCode.InternalServerError, null, 'Internal Server Error'));
}

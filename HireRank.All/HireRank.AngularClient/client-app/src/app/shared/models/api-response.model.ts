export interface ApiResponse {
  statusCode : number;
  data : object;
  errorCode : number;
  details : string;
  validationErrors : string;
}

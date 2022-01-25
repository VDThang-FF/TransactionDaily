export default interface ServiceResponse {
    Success: boolean,
    Code: int,
    SubCode: int,
    Data: object,
    Message: string,
    DevMessage: string
}
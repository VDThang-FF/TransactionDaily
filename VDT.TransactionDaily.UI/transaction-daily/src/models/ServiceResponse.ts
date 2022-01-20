import ENUM from '@/scripts/enums';

export default interface ServiceResponse {
    Success: boolean = true,
    Code: int = ENUM.Code.Success,
    SubCode: int = ENUM.SubCode.Success,
    Data: object,
    Message: string,
    DevMessage: string
}
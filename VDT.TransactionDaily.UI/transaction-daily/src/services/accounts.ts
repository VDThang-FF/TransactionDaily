import _httpClient from './http-common';
import BaseAPIS from './base-services';

class AccountAPIS extends BaseAPIS {
    constructor(subAPIUrl: string, subSystemCode: string = null) {
        super(subAPIUrl, subSystemCode);
    }

    /**
     * API đăng nhập
     * @param data 
     * created by vdthang 20.01.2022
     */
    login(data: any): Promise<any> {
        return _httpClient.post(this._subAPIUrl + "/Login", data);
    }
}

export default AccountAPIS;
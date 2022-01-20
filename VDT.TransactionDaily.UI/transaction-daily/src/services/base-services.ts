import _httpClient from './http-common';
import { IsNullOrWhiteSpace } from '@/scripts/helpers';

class BaseAPIS {
    _subAPIUrl: string = null;
    _subSystemCode: string = null;

    constructor(subAPIUrl: string, subSystemCode: string = null) {
        if (IsNullOrWhiteSpace(subAPIUrl))
            throw new Error("Chưa gán sub url call API");
        this._subAPIUrl = subAPIUrl;

        this._subSystemCode = subSystemCode;
    }

    /**
     * API lấy tất cả danh sách
     * created by vdthang 20.01.2022
     */
    getAll(): Promise<any> {
        return _httpClient.get(this._subAPIUrl)
    }
}

export default BaseAPIS;
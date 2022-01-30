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
     * @returns
     */
    getAll(): Promise<any> {
        return _httpClient.get(this._subAPIUrl)
    }

    /**
     * API thêm mới
     * @param model
     * @returns
     * created by vdthang 30.01.2022 
     */
    insert(model: any): Promise<any> {
        return _httpClient.post(this._subAPIUrl, model);
    }

    /**
     * API cập nhật
     * @param model 
     * @returns 
     * created by vdthang 30.01.2022
     */
    update(model: any): Promise<any> {
        return _httpClient.put(this._subAPIUrl, model);
    }
}

export default BaseAPIS;
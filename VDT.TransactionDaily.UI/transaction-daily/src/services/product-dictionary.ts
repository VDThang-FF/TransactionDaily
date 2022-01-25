import _httpClient from './http-common';
import BaseAPIS from './base-services';

class ProductDictionaryAPIS extends BaseAPIS {
    constructor(subAPIUrl: string = "ProductDictionaries", subSystemCode: string = null) {
        super(subAPIUrl, subSystemCode);
    }
}

export default ProductDictionaryAPIS;
import axios from 'axios';

class RestClientFactory {
  headers = {
    'X-Caller-Scopes': 'gaia-logistics-ui',
    'X-Scope-Id': 'gaia-logistics-ui',
    'Accept': 'application/json',
    'Content-Type': 'application/json',
  };
  timeout = 3000;
  baseUrl = '';

  constructor(url = '') {
    this.baseUrl = url;
  }

  setHeaders(headers) {
    this.headers = headers;
    return this;
  }

  setTimeout(timeout) {
    this.timeout = timeout;
    return this;
  }

  setBaseUrl(baseUrl) {
    this.baseUrl = baseUrl;
    return this;
  }

  extendHeaders(headers) {
    this.headers = { ...this.headers, ...headers };
    return this;
  }

  create() {
    return axios.create({
      baseURL: this.baseUrl,
      timeout: this.timeout,
      headers: this.headers,
    });
  }
}

export default RestClientFactory;

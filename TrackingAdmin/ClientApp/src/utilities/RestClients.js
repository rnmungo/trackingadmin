import RestClientFactory from './RestClientFactory';

export const coreRestClient = new RestClientFactory('/api').create();

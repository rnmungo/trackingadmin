export const convertToQueryParams = object => {
  const params = Object.entries(object)
    .filter(([_, value]) => !!value)
    .map(
      ([key, value]) =>
        `${encodeURIComponent(key)}=${encodeURIComponent(value)}` 
    )
    .join('&');
  return params;
};

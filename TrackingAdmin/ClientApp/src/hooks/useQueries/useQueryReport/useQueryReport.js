import { useState } from 'react';
import { useQuery } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';
import { convertToQueryParams } from '../../../utilities';

const getReport = async filters => {
  const queryParams = convertToQueryParams(filters);
  const response = await coreRestClient.get(`/roadmap/search?${queryParams}`);
  return response.data;
};

const useQueryReport = defaultFilters => {
  const [filtersState, setFiltersState] = useState(defaultFilters);
  const { status, data, refetch, error } = useQuery(
    ['report', filtersState],
    () => getReport(filtersState),
    {
      enabled: true,
      staleTime: 60000,
      refetchOnWindowFocus: false,
    }
  );

  return {
    setFilters: setFiltersState,
    filters: filtersState,
    status,
    data,
    refetch,
    error,
  };
};

export default useQueryReport;

import { useQuery } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const getAllowedTrucks = async () => {
  const response = await coreRestClient.get('/truck/allowed');
  return response.data;
};

const useQueryAllowedTrucks = () => {
  const { status, data, refetch, error } = useQuery(
    ['allowed-trucks'],
    () => getAllowedTrucks(),
    {
      enabled: true,
      refetchOnWindowFocus: false,
    }
  );

  return {
    status,
    data,
    refetch,
    error,
  };
};

export default useQueryAllowedTrucks;

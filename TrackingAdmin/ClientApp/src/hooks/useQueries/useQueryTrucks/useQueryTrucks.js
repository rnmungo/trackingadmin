import { useQuery } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const getTrucks = async () => {
  const response = await coreRestClient.get('/truck');
  return response.data;
};

const useQueryTrucks = () => {
  const { status, data, refetch, error } = useQuery(
    ['trucks'],
    () => getTrucks(),
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

export default useQueryTrucks;

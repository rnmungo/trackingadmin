import { useQuery } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const getLocations = async () => {
  const response = await coreRestClient.get('/location');
  return response.data;
};

const useQueryLocations = ({ key }) => {
  const { status, data, refetch, error } = useQuery(
    [`locations${key ? `-${key}` : ''}`],
    () => getLocations(),
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

export default useQueryLocations;

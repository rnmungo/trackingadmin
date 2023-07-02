import { useMutation } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const createRoadMap = ({ truckId, travels }) =>
  coreRestClient.post('/roadmap', {
    truckId,
    travels,
  });

const useMutationCreateRoadMap = () => {
  const { status, error, mutate, reset, data } = useMutation(createRoadMap);

  return {
    status,
    error,
    mutate,
    reset,
    data,
  };
};

export default useMutationCreateRoadMap;

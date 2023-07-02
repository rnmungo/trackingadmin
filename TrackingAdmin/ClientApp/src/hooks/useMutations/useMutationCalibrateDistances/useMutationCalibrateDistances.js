import { useMutation } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const calibrateDistances = async ({ originId, destinationIds }) => {
  const response = await coreRestClient.post('/distance/calibrate', {
    originId,
    destinationIds,
  });
  return response.data;
}

const useMutationCalibrateDistances = () => {
  const { status, error, mutate, reset, data } = useMutation(calibrateDistances);

  return {
    status,
    error,
    mutate,
    reset,
    data,
  };
};

export default useMutationCalibrateDistances;

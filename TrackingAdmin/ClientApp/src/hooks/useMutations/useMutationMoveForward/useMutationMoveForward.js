import { useMutation } from 'react-query';
import { coreRestClient } from '../../../utilities/RestClients';

const moveForward = ({ roadMapId }) =>
  coreRestClient.put(`/roadmap/${roadMapId}/move-forward`);

const useMutationMoveForward = () => {
  const { status, error, mutate, reset, data } = useMutation(moveForward);

  return {
    status,
    error,
    mutate,
    reset,
    data,
  };
};

export default useMutationMoveForward;

import { useEffect } from 'react';
import { func, string } from 'prop-types';
import { Dropdown } from '../../components/dropdown';
import { useQueryAllowedTrucks } from '../../hooks/useQueries';

const AllowedTrucksDropdownContainer = ({
  value = '',
  onChange = () => { },
}) => {
  const { data, status, error, refetch } = useQueryAllowedTrucks();

  useEffect(() => {
    if (!!value) {
      refetch();
    }
  }, [refetch, value]);

  return (
    <Dropdown
      sx={{ width: 320 }}
      label="Camiones disponibles"
      ariaLabel="trucks"
      helperText={status === 'error' && (error.response?.data?.message || error.message)}
      value={value}
      items={data ? data.map(({ id, licensePlate, model }) => ({ id, text: `${model} (${licensePlate})` })) : []}
      onChange={onChange}
    />
  );
};

AllowedTrucksDropdownContainer.propTypes = {
  value: string,
  onChange: func,
};

export default AllowedTrucksDropdownContainer;

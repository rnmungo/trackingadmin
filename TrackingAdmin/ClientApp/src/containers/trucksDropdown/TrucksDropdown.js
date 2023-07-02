import { func, oneOf, string } from 'prop-types';
import { Dropdown } from '../../components/dropdown';
import { useQueryTrucks } from '../../hooks/useQueries';

const TrucksDropdownContainer = ({
  keyField = 'id',
  value = '',
  onChange = () => { },
}) => {
  const { data, status, error } = useQueryTrucks();

  return (
    <Dropdown
      sx={{ width: 240 }}
      label="Camiones"
      ariaLabel="trucks"
      helperText={status === 'error' && (error.response?.data?.message || error.message)}
      value={value}
      items={data ? data.map(truck => ({ id: truck[`${keyField}`], text: `${truck.model} (${truck.licensePlate})` })) : []}
      onChange={onChange}
    />
  );
};

TrucksDropdownContainer.propTypes = {
  keyField: oneOf(['id', 'licensePlate']),
  value: string,
  onChange: func,
};

export default TrucksDropdownContainer;

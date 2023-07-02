import { func, string } from 'prop-types';
import { Dropdown } from '../../components/dropdown';
import { STATUS } from '../../constants';

const StatusDropdownContainer = ({
  value = '',
  onChange = () => { },
}) => {

  return (
    <Dropdown
      sx={{ width: 240 }}
      label="Estado"
      ariaLabel="status"
      value={value}
      items={STATUS.map(({ id, text }) => ({ id, text }))}
      onChange={onChange}
    />
  );
};

StatusDropdownContainer.propTypes = {
  value: string,
  onChange: func,
};

export default StatusDropdownContainer;

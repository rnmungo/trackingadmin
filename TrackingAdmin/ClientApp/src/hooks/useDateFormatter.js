import isValid from 'date-fns/isValid';
import format from 'date-fns/format';
import formatRFC3339 from 'date-fns/formatRFC3339';
import es from 'date-fns/locale/es';

const useDateFormatter = () => {
  const getDateTimeLocalString = date =>
    isValid(date)
      ? format(new Date(date), 'dd/MM/yyyy HH:mm', { locale: es })
      : '';

  const getRFC3339 = date =>
    isValid(date)
      ? formatRFC3339(date, { locale: es })
      : '';

  return { getDateTimeLocalString, getRFC3339 };
};

export default useDateFormatter;

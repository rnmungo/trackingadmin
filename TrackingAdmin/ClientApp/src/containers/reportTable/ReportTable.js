import MuiBox from '@mui/material/Box';
import MuiStack from '@mui/material/Stack';
import MuiPaper from '@mui/material/Paper';
import MuiTable from '@mui/material/Table';
import MuiTableBody from '@mui/material/TableBody';
import MuiTableCell from '@mui/material/TableCell';
import MuiTableContainer from '@mui/material/TableContainer';
import MuiTablePagination from '@mui/material/TablePagination';
import MuiTableHead from '@mui/material/TableHead';
import MuiTableRow from '@mui/material/TableRow';
import { ReportTableRow } from './reportTableRow';
import { Spinner } from '../../components/spinner';
import { useQueryReport } from '../../hooks/useQueries';
import { TrucksDropdownContainer } from '../trucksDropdown';
import { StatusDropdownContainer } from '../statusDropdown';

const DEFAULT_FILTERS = {
  status: 'Created',
  license: '',
  sizeLimit: 10,
  currentPage: 1,
};

const ReportTableContainer = () => {
  const { data, status, error, filters, setFilters } = useQueryReport({
    ...DEFAULT_FILTERS,
  });

  const handleChangeTruck = value => {
    setFilters(prevState => ({ ...prevState, license: value }));
  };

  const handleChangeStatus = value => {
    setFilters(prevState => ({ ...prevState, status: value }));
  };

  const handleChangePage = (event, newPage) => {
    setFilters(prevState => ({ ...prevState, currentPage: newPage + 1 }));
  };

  const handleChangeRowsPerPage = (event) => {
    setFilters(prevState => ({ ...prevState, sizeLimit: parseInt(event.target.value, 10), currentPage: 1 }));
  };

  return (
    <>
      <Spinner loading={['loading', 'refetching'].includes(status)} label="Cargando datos..." />
      {status === 'error' && <div>{error.message}</div>}
      <MuiStack direction="row" spacing={2} sx={{ my: 2 }}>
        <StatusDropdownContainer value={filters.status} onChange={handleChangeStatus} />
        <TrucksDropdownContainer keyField="licensePlate" value={filters.truckId} onChange={handleChangeTruck} />
      </MuiStack>
      {status === 'success' && data && (
        <MuiBox sx={{ my: 2 }}>
          <MuiTableContainer component={MuiPaper}>
            <MuiTable aria-label="collapsible table">
              <MuiTableHead>
                <MuiTableRow>
                  <MuiTableCell />
                  <MuiTableCell align="left">Nr.</MuiTableCell>
                  <MuiTableCell align="left">Camión</MuiTableCell>
                  <MuiTableCell align="left">Estado</MuiTableCell>
                  <MuiTableCell />
                </MuiTableRow>
              </MuiTableHead>
              <MuiTableBody>
                {data.results.length > 0 && data.results.map(row => (
                  <ReportTableRow key={row.id} row={row} />
                ))}
                {data.results.length === 0 && (
                  <MuiTableRow>
                    <MuiTableCell component="th" scope="row" align="center" colSpan={5}>
                      No hay datos para mostrar
                    </MuiTableCell>
                  </MuiTableRow>
                )}
              </MuiTableBody>
            </MuiTable>
          </MuiTableContainer>
          <MuiTablePagination
            rowsPerPageOptions={[5, 10, 25]}
            component="div"
            count={data.total}
            rowsPerPage={filters.sizeLimit}
            page={filters.currentPage - 1}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
            labelDisplayedRows={({ from, to, count }) =>
              `${from}-${to} de ${count !== -1 ? count : `más de ${to}`}`
            }
            labelRowsPerPage="Filas por página:"
          />
        </MuiBox>
      )}
    </>
  );
};

export default ReportTableContainer;

import { node, oneOfType, string } from 'prop-types';
import { QueryClient, QueryClientProvider } from 'react-query';

const queryClient = new QueryClient();

const QueryProvider = ({ children }) => (
  <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
);

QueryProvider.propTypes = {
  children: oneOfType([node, string]).isRequired,
};

export default QueryProvider;

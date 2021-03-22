import { createFetch } from '../composition';

const baseUri =
  process.env.NODE_ENV === 'development'
    ? 'https://localhost:5001/api'
    : 'http://localhost:3001/api';

export const apiClient = createFetch(baseUri);

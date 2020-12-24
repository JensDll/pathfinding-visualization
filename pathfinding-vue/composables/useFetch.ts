import { ref } from 'vue';

// This does not cover all fetch functionality,
// currently only works with JSON response
export function useFetch<TJson, TError = unknown>(
  request?: RequestInfo,
  init?: RequestInit
) {
  const json = ref<TJson>();
  const loading = ref(false);
  const error = ref<TError>();
  const response = ref<Response>();

  const exec = async (request: RequestInfo, init?: RequestInit) => {
    loading.value = true;
    console.log('FETCH');
    try {
      const res = await fetch(request, init);
      response.value = res;

      if (!res.ok) {
        console.log(response);
      } else {
        if (res.status !== 204 && res.status !== 201) {
          json.value = await res.json();
        }
      }
    } catch (err) {
      console.log(err);

      error.value = err;
    } finally {
      loading.value = false;
    }
  };

  if (request) {
    exec(request, init);
  }

  return {
    json,
    loading,
    error,
    response,
    exec
  };
}

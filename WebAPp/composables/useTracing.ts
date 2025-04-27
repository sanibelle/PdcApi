import { trace } from '@opentelemetry/api';

const useTracing = () => {
  const tracer = trace.getTracer('nuxt-tracer');

  const startSpan = async (spanName: string, callback: () => Promise<void>) => {
    if (process.env.NODE_ENV !== 'production') return;
    const span = tracer.startSpan(spanName);
    try {
      await callback();
    } catch (err: any) {
      span.recordException(err);
      throw err;
    } finally {
      span.end();
    }
  };

  return {
    startSpan,
  };
};

export default useTracing;

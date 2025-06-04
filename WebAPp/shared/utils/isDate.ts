export const isDate = (date: string | Date) => {
  return (
    date &&
    (date instanceof Date ||
      (typeof date === 'string' && !isNaN(Date.parse(date)) && !isNaN(new Date(date).getTime())))
  );
};

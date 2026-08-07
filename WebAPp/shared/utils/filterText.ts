export const filterText = (items: any[], searchText: string, fields: string[]): any[] => {
  const cleanSearchText = searchText
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .toLowerCase();

  return items.filter((item) => {
    return fields.some((field) => {
      if (!item[field]) return false;

      const cleanFieldValue = item[field]
        .toString()
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase();

      return cleanFieldValue.includes(cleanSearchText);
    });
  });
};

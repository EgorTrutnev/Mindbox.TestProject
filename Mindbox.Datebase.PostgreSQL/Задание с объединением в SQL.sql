SELECT
  pr."Name" ProductName,
  ca."Name" CategoryName
FROM public."Products" pr
LEFT JOIN public."CategoryProduct" capr
  ON pr."Id" = capr."ProductsId"
LEFT JOIN public."Categories" ca
  ON ca."Id" = capr."CategoriesId"
ORDER BY pr."Name", ca."Name";

/*ИЛИ*/

SELECT
  pr."Name" ProductName,
  ca."Name" CategoryName
FROM public."CategoryProduct" capr
RIGHT JOIN public."Products" pr
  ON pr."Id" = capr."ProductsId"
LEFT JOIN public."Categories" ca
  ON ca."Id" = capr."CategoriesId"
ORDER BY pr."Name", ca."Name";
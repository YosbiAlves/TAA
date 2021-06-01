To process a data document, the name of the file to be read in the main() of AVL_tree.cpp must be modified. 

The format this file must be in is:

	operation ( number1 number2...)

where operation can be: 
	insert + elements (inserts elements) 
	remove + elements (removes elements) 
	search + elements (searchs elements in the tree) 
	max (shows the maximum element of the tree) 
	min (shows the minimum element of the tree)
	display or inorder (shows the tree with traversal inorder)
	postorder (shows the tree with traversal postorder)
	preorder (shows the tree with traversal preorder)
	clear (removes completely the tree)

An example would be:
	insert 12 4 2 1
	inorder
although it would also be valid:
	insert 33 , 42 , 2 , 323
	inorder
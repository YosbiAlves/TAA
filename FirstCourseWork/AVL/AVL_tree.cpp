/* AVL Tree Implementation in C++   
    AVL_tree.cpp
    AVL_tree

    Created by Marta Gómez on 24/03
*/

#include<iostream>
#include<fstream>
#include<string>
#include<cstdlib>
#include<cstring>
#include<chrono>

using namespace std;

class AVL_tree{
    struct node{
        int data; 
        node* left;  
        node* right; 
        int height; 
    };

    node* root; 
    bool found = false;

    void makeEmpty(node* t){
        if(t == NULL)
            return;
        makeEmpty(t->left);
        makeEmpty(t->right);
        delete t;
    }

    node* insert(int x, node* t){
        if(t == NULL){
            t = new node;
            t->data = x;
            t->height = 0;
            t->left = t->right = NULL;
        }
        else if(x < t->data){
            t->left = insert(x, t->left);
            if(height(t->left) - height(t->right) == 2){
                if(x < t->left->data)
                    t = singleRightRotate(t);
                else
                    t = doubleRightRotate(t);
            }
        }
        else if(x > t->data){
            t->right = insert(x, t->right);
            if(height(t->right) - height(t->left) == 2)
            {
                if(x > t->right->data)
                    t = singleLeftRotate(t);
                else
                    t = doubleLeftRotate(t);
            }
        }

        t->height = max(height(t->left), height(t->right))+1;
        return t;
    }

    node* singleRightRotate(node* &t){
        node* u = t->left;
        t->left = u->right;
        u->right = t;
        t->height = max(height(t->left), height(t->right))+1;
        u->height = max(height(u->left), t->height)+1;
        return u;
    }

    node* singleLeftRotate(node* &t){
        node* u = t->right;
        t->right = u->left;
        u->left = t;
        t->height = max(height(t->left), height(t->right))+1;
        u->height = max(height(t->right), t->height)+1 ;
        return u;
    }

    node* doubleLeftRotate(node* &t){
        t->right = singleRightRotate(t->right);
        return singleLeftRotate(t);
    }

    node* doubleRightRotate(node* &t){
        t->left = singleLeftRotate(t->left);
        return singleRightRotate(t);
    }

    node* findMin(node* t){
        if(t == NULL)
            return NULL;
        else if(t->left == NULL)
            return t;
        else
            return findMin(t->left);
    }

    node* findMax(node* t){
        if(t == NULL)
            return NULL;
        else if(t->right == NULL)
            return t;
        else
            return findMax(t->right);
    }

    node* remove(int x, node* t){
        node* temp;
        if(t == NULL)
            return NULL;

        else if(x < t->data)
            t->left = remove(x, t->left);
        else if(x > t->data)
            t->right = remove(x, t->right);

        // element found with 2 child
        else if(t->left && t->right){
            temp = findMin(t->right);
            t->data = temp->data;
            t->right = remove(t->data, t->right);
        }
        // element found with 1 or 0 child
        else{
            temp = t;
            if(t->left == NULL)
                t = t->right;
            else if(t->right == NULL)
                t = t->left;
            delete temp;
        }
        if(t == NULL)
            return t;

        t->height = max(height(t->left), height(t->right))+1;

        // not balanced
        // left node deleted, right case
        if(height(t->left) - height(t->right) == 2){
            // right right case
            if(height(t->left->left) - height(t->left->right) == 1)
                return singleLeftRotate(t);
            // right left case
            else
                return doubleLeftRotate(t);
        }
        // right node deleted, left case
        else if(height(t->right) - height(t->left) == 2){
            // left left case
            if(height(t->right->right) - height(t->right->left) == 1)
                return singleRightRotate(t);
            // left right case
            else
                return doubleRightRotate(t);
        }
        return t;
    }

    int height(node* t){
        if (t==NULL)
            return -1;
        else 
            return t->height;
    }

    int getBalance(node* t){
        if(t == NULL)
            return 0;
        else
            return height(t->left) - height(t->right);
    }
    
    void inorder(node* t){
        if(t != NULL){ 
            inorder(t->left);
            cout << t->data << " ";
            inorder(t->right);
        }
    }

    void postorder(node* t){
        if(t != NULL){ 
            postorder(t->left);
            postorder(t->right);
            cout << t->data << " ";
        }
    }

    void preorder(node* t){
        if(t != NULL){ 
            cout << t->data << " ";
            preorder(t->left);
            preorder(t->right);
        }
    }
 
    bool find(node* t, int x){
        if(t != NULL ){  
            if (t->data == x){ 
                found = true;
            }
            else if (t->data < x){ 
                find(t->right, x); 
            }
            else {
                find(t->left, x); 
            }
        }
        return found;
    } 


public:
    AVL_tree(){
        root = NULL;
    }

    void insert(int x){
        root = insert(x, root);
        //cout << x << " inserted " << endl;
    }

    void remove(int x){
        root = remove(x, root);
        //cout << x << " removed " << endl;
    }

    void clear(){
        makeEmpty(root);
        root = NULL;
        cout << "Tree clear" << endl;
    }

    void display(){
        cout << "Inorder: " ;
        if (root==NULL)
            cout << "AVL NULL" << endl;
        else{ 
            inorder(root); 
            cout << endl;
        }
    }

    void display_post(){
        cout << "Postorder: " ;
        if (root==NULL)
            cout << "AVL NULL" << endl;
        else{ 
            postorder(root); 
            cout << endl;
        }
    }

    void display_pre(){
        cout << "Preorder: " ;
        if (root==NULL)
            cout << "AVL NULL" << endl;
        else{ 
            preorder(root); 
            cout << endl;
        }
    }

    void min_AVL(){
        if (root != NULL){
            int min = findMin(root)->data;
            cout << "Min: "<< min << endl;
        }
        else 
            cout << "Min: empty" << endl; 
    }

    void max_AVL(){
        if (root != NULL){
            int max = findMax(root)->data;
            cout << "Max: "<< max << endl;
        }
        else 
            cout << "Max: empty" << endl; 
    }

    void search(int x){
        found = false;
        bool search = find(root,x);
        if (search == true) { 
            cout << x << " found" << endl; 
        }
        else { 
            cout << x << " not found" << endl;
        }
    }
};

int main(){ 
    
    FILE *fp1;
    char palabra[100];
    char orden[100];
    int num;
    int c;
    AVL_tree t;

    auto begin = std::chrono::steady_clock::now();
    fp1 = fopen("data4.txt", "r");
    c = fscanf(fp1, "%s", palabra);
    while (c != EOF){
        if (strcmp(palabra,"insert")==0){
            strcpy(orden, palabra);
        }

        if (strcmp(palabra,"remove")==0){
            strcpy(orden, palabra);
        }

        if (strcmp(palabra,"search")==0){
            strcpy(orden, palabra);
        }

        if (strcmp(palabra,"max")==0){
            t.max_AVL();
        }

        if (strcmp(palabra,"min")==0){
            t.min_AVL();
        }

        if (strcmp(palabra,"display")==0 || strcmp(palabra,"inorder")==0){
            t.display();
        }

        if (strcmp(palabra,"postorder")==0){
            t.display_post();
        }

        if (strcmp(palabra,"preorder")==0){
            t.display_pre();
        }

        if (strcmp(palabra,"clear")==0){
            t.clear();
        }

        if (atoi(palabra)!=0){
            if (strcmp(orden,"insert")==0){
                num = atoi(palabra);
                t.insert(num);
            }
            if (strcmp(orden,"remove")==0){
                num = atoi(palabra);
                t.remove(num);
            }
            if (strcmp(orden,"search")==0){
                num = atoi(palabra);
                t.search(num);
            }
        }
        c = fscanf(fp1, "%s", palabra);
        //printf("%s\n", palabra); 
    }
    fclose(fp1);
    auto end = std::chrono::steady_clock::now();
    auto elapsed = std::chrono::duration<double>(end - begin);
    cout << "Elapsed_time: " << elapsed.count() << "s" << endl;
    return 0;
    
}
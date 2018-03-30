<template>
  <div>
    <template v-for="(item, index) in value">
      <div
        :key="index"
        class="my2 right-align">
        <Btn
          :key="index"
          label="Remover"
          danger
          @click="remove(item)" />
      </div>
      <slot
        :item="item"
        :error="errorsFor(index)" />
    </template>
    <div
      v-if="!value.length"
      class="bg-silver thin rounded p2 center">
      Nenhum item! Clique em "Inserir" para adicionar um novo.
    </div>
    <div class="my2">
      <Btn
        label="Inserir"
        primary
        class="block col-12"
        @click="add" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Multi",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      errors: {
        type: Object,
        default: () => [],
      },
      errorKey: {
        type: String,
        default: "",
      },
      default: {
        type: Object,
        default: () => ({}),
      },
    },
    methods: {
      add() {
        const value = [...this.value, this.default];
        this.$emit("input", value);
      },
      remove(item) {
        const values = this.value.filter(a => a !== item);
        this.$emit("input", values);
      },
      errorsFor(index) {
        const errors = this.errors && this.errors[`${this.errorKey}[${index}]`];
        return errors || {};
      },
    },
  };
</script>

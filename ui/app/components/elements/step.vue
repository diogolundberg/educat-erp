<template>
  <div class="mb3">
    <div
      :class="{ pointer: !disabled }"
      class="px2 py3 bg-white shadow1 rounded flex items-center"
      @click="$parent.goTo(index)">
      <Ball class="mr2">
        {{ index }}
      </Ball>
      <div class="border-right border-silver y3 mr2" />
      <div class="h5 line-height-3 flex-auto">
        <div class="green bold h3">{{ title }}</div>
        <div class="h4 mt1 dim">{{ description }}</div>
      </div>
      <Badge
        v-if="complete"
        success
        big>
        Finalizado
      </Badge>
      <Badge
        v-else-if="error"
        error
        big>
        Erro
      </Badge>
      <Badge
        v-else-if="warning"
        warning
        big>
        Atenção
      </Badge>
      <Badge
        v-else
        big>
        Não preenchido
      </Badge>
    </div>
    <div
      v-if="visible && !disabled"
      class="shadow1 rounded bg-silver p2">
      <div class="bg-white p2">
        <slot />
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Step",
    props: {
      title: {
        type: String,
        required: true,
      },
      description: {
        type: String,
        required: true,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      complete: {
        type: Boolean,
        default: false,
      },
      error: {
        type: Boolean,
        default: false,
      },
      warning: {
        type: Boolean,
        default: false,
      },
    },
    computed: {
      visible() {
        return this.$parent.value === this.index;
      },
      index() {
        return this.$parent.$slots.default.filter(s => s.tag)
          .indexOf(this.$vnode) + 1;
      },
      current() {
        return this.$parent.value;
      },
    },
  };
</script>
